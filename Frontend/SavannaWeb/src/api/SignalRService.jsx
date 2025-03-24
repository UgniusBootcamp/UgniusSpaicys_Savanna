import {
  HttpTransportType,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';
import endpointConstants from '../constants/endpointConstants';
import errorConstants from '../constants/errorConstants';

class SignalRService {
  constructor() {
    this.connection = null;
  }

  async startConnection(url) {
    const token = localStorage.getItem('accessToken');
    console.log(url);

    this.connection = new HubConnectionBuilder()
      .withUrl(url, {
        accessTokenFactory: () => token,
        transport: HttpTransportType.WebSockets | HttpTransportType.LongPolling,
      })
      .configureLogging(LogLevel.Information)
      .withAutomaticReconnect()
      .build();

    try {
      await this.connection.start();
    } catch (err) {
      console.error(errorConstants.signalRConnectionFail, err);
    }
  }

  async stopConnection() {
    if (this.connection) {
      await this.connection.stop();
    }
  }

  async invokeCreateGame(gameCreateDto) {
    if (this.connection) {
      try {
        await this.connection.invoke(
          endpointConstants.createGame,
          gameCreateDto,
        );
      } catch (err) {
        console.error(errorConstants.createGameInvokeFail, err);
      }
    }
  }

  async invokeCreateAnimal(animalTypeId) {
    if (this.connection) {
      try {
        await this.connection.invoke(
          endpointConstants.createAnimal,
          animalTypeId,
        );
      } catch (err) {
        console.error(errorConstants.createAnimalInvokeFail, err);
      }
    }
  }

  async pauseGame() {
    if (this.connection) {
      try {
        await this.connection.invoke(endpointConstants.pauseGame);
      } catch (err) {
        console.error(errorConstants.pauseGameFail);
      }
    }
  }

  async resumeGame() {
    if (this.connection) {
      try {
        await this.connection.invoke(endpointConstants.resumeGame);
      } catch (err) {
        console.error(errorConstants.resumeGameFail);
      }
    }
  }
  async saveGame() {
    if (this.connection) {
      try {
        await this.connection.invoke('SaveGame');
      } catch (err) {
        console.error(errorConstants.resumeGameFail);
      }
    }
  }
  async loadGame(gameId) {
    if (this.connection) {
      try {
        await this.connection.invoke('LoadGame', gameId);
      } catch {
        console.error(errorConstants.loadGameFail);
      }
    }
  }
}

export default new SignalRService();
