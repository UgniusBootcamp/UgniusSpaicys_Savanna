import * as signalR from '@microsoft/signalr';
import { useEffect, useState } from 'react';
import SignalRService from '../../api/SignalRService';
import { Spinner } from '../../components/common/Spinner';
import Game from '../../components/game/Game';
import GameActions from '../../components/game/GameActions';
import GameCreationForm from '../../components/game/GameCreationForm';
import GameLoader from '../../components/game/GameLoader';
import GameStats from '../../components/game/GameStats';

const Savanna = () => {
  const gameConnectionUrl = import.meta.env.VITE_API_GAME_URL;
  const [game, setGame] = useState(null);
  const [connectionEstablished, setConnectionEstablished] = useState(false);

  useEffect(() => {
    const connectToGameHub = async () => {
      try {
        await SignalRService.startConnection(gameConnectionUrl);
        setConnectionEstablished(true);
        console.log(SignalRService.connection.connectionId);
      } catch (error) {
        console.error('Error while connecting to SignalR:', error);
      }
    };

    connectToGameHub();

    return async () => {
      if (
        SignalRService.connection.state === signalR.HubConnectionState.Connected
      ) {
        await SignalRService.stopConnection();
        console.log('Unmaut');
      }
    };
  }, []);

  if (!connectionEstablished) return <Spinner />;

  return (
    <div className="grid grid-cols-7 grid-rows-7 gap-3 w-full h-full text-white">
      <div className="col-span-7 bg-primary-400 rounded-md h-12 w-full flex justify-center items-center">
        <GameStats />
      </div>
      <div className="col-span-5 row-start-2 bg-primary-400 rounded-md flex justify-center items-center">
        <GameActions />
      </div>
      <div className="col-span-5 row-span-5 col-start-1 row-start-3">
        {game ? <Game /> : <GameCreationForm />}
      </div>
      <div className="col-span-2 row-span-6 col-start-6 row-start-2 bg-primary-400 rounded-md flex justify-center items-center">
        <GameLoader />
      </div>
    </div>
  );
};

export default Savanna;
