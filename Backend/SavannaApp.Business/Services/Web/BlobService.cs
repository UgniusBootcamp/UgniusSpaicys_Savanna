﻿using System.Text;
using System.Text.Json;
using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Interfaces.Web;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Dto.Game;
using SavannaApp.Data.Dto.Game.Save;
using SavannaApp.Data.Entities;
using SavannaApp.Data.Helpers.Configuration;

namespace SavannaApp.Business.Services.Web
{
    public class BlobService(IOptions<AzureBlobServiceOptions> options, IDtoMapper dtoMapper, IMapper mapper) : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient = new(options.Value.Key);

        /// <summary>
        /// Method to save game to azure blob storage in json
        /// </summary>
        /// <param name="game">game to save</param>
        public async Task SaveGameAsync(Game game)
        {
            var userContainer = _blobServiceClient.GetBlobContainerClient(game.UserId);
            await userContainer.CreateIfNotExistsAsync();

            var blobClient = userContainer.GetBlobClient(String.Format("{0}{1}", game.Id.ToString(), WebServiceConstants.Json));

            var gameSave = dtoMapper.MapGameSave(game);

            var gameJson = JsonSerializer.Serialize(gameSave, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(gameJson));
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        /// <summary>
        /// Method to load game from azure blob storage
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="gameId">game id</param>
        public async Task<Game?> LoadGameAsync(string userId, string gameId)
        {
            var userContainer = _blobServiceClient.GetBlobContainerClient(userId);
            if (!await userContainer.ExistsAsync()) return null;

            var blobClient = userContainer.GetBlobClient(String.Format("{0}{1}", gameId.ToString(), WebServiceConstants.Json));

            if(await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadStreamingAsync();

                using var streamReader = new StreamReader(response.Value.Content);
                var json = await streamReader.ReadToEndAsync();

                var gameSave = JsonSerializer.Deserialize<GameSaveDto>(json);

                if(gameSave != null)
                    return dtoMapper.MapGame(gameSave);
            }

            return null;
        }

        /// <summary>
        /// Get previous user games
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="start">start date</param>
        /// <param name="end">end date</param>
        /// <returns>get previous games in date ranges</returns>
        public async Task<IEnumerable<GameLoadInfoDto>> GetUserGamesAsync(string userId, DateOnly start, DateOnly end)
        {
            var userContainer = _blobServiceClient.GetBlobContainerClient(userId);
            if (!await userContainer.ExistsAsync()) return [];

            var games = new List<GameLoadInfoDto>();
            var blobs = userContainer.GetBlobsAsync();

            var startDateTime = start.ToDateTime(TimeOnly.MinValue);
            var endDateTime = end.ToDateTime(TimeOnly.MaxValue);

            await foreach (var blobItem in blobs)
            {
                var lastModified = blobItem.Properties.LastModified?.DateTime;
                if (lastModified is null || lastModified < startDateTime || lastModified > endDateTime)
                    continue;

                var blobClient = userContainer.GetBlobClient(blobItem.Name);
                var response = await blobClient.DownloadAsync();

                using var streamReader = new StreamReader(response.Value.Content);
                var json = await streamReader.ReadToEndAsync();

                var gameSave = JsonSerializer.Deserialize<GameSaveDto>(json);
                if (gameSave != null)
                {
                    var mapped = mapper.Map<GameLoadInfoDto>(dtoMapper.MapGame(gameSave));
                    mapped.LastModified = lastModified.Value.ToLocalTime();
                    games.Add(mapped);
                }
            }

            return games;
        }

    }
}
