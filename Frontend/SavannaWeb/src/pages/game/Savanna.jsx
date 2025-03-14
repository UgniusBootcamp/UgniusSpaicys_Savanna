import * as signalR from '@microsoft/signalr';
import { useEffect, useState } from 'react';
import SignalRService from '../../api/SignalRService';
import { Spinner } from '../../components/common/Spinner';
import Game from '../../components/game/Game';
import GameCreationForm from '../../components/game/GameCreationForm';
import SavannaHeader from '../../components/game/SavannaHeader';
import useErrorHandler from '../../hooks/useErrorHandler';

const Savanna = () => {
  const gameConnectionUrl = import.meta.env.VITE_API_GAME_URL;
  const [game, setGame] = useState(null);
  const [connectionEstablished, setConnectionEstablished] = useState(false);
  const errorHandler = useErrorHandler();

  useEffect(() => {
    const connectToGameHub = async () => {
      try {
        await SignalRService.startConnection(gameConnectionUrl);
        setConnectionEstablished(true);
      } catch (error) {
        errorHandler.handleError(error);
      }
    };

    connectToGameHub();

    return async () => {
      if (
        SignalRService.connection.state === signalR.HubConnectionState.Connected
      ) {
        await SignalRService.stopConnection();
      }
    };
  }, []);

  useEffect(() => {
    const handleGameDataReceived = (gameData) => {
      setGame(gameData);
    };

    SignalRService.connection.on('ReceiveGameData', handleGameDataReceived);
  }, []);

  if (!connectionEstablished) return <Spinner />;

  if (!game) return <GameCreationForm />;

  return (
    <div className="flex flex-col gap-3 w-full h-screen text-white">
      <SavannaHeader game={game} />
      <div className="h-2/3 w-full m-2 flex justify-center">
        <Game map={game.map} />
      </div>
    </div>
  );
};

export default Savanna;
