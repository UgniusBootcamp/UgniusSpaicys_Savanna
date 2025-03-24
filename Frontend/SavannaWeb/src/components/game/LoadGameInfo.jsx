import PropTypes from 'prop-types';
import SignalRService from '../../api/SignalRService';
import gameCreationConstants from '../../constants/gameCreationConstants';
import { useAuth } from '../../hooks/useAuth';

const LoadGameInfo = ({ game }) => {
  const { setSnackbarMessage } = useAuth();

  const handleGameLoad = async (gameId) => {
    await SignalRService.loadGame(gameId);
    setSnackbarMessage(gameCreationConstants.loadSuccessful);
  };

  return (
    <div className="flex flex-col bg-primary-200 rounded-xl p-3 shadow-sm space-y-3 w-fit text-sm w-full">
      <div className="flex flex-col">
        <span className="text-base  font-semibold text-primary-900">
          {gameCreationConstants.iteration} {game.iteration}
        </span>{' '}
        <span>
          {new Date(game.lastModified).toLocaleString(undefined, {
            year: 'numeric',
            month: 'short',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit',
            hour12: false,
          })}
        </span>
      </div>

      <div className="flex flex-wrap gap-2">
        {Object.entries(game.animalCount).map(([animal, count]) => (
          <div
            key={animal}
            className="bg-white rounded-lg px-3 py-2 shadow-sm flex items-center justify-between border border-primary-300 text-sm gap-0.5"
          >
            <span className="text-primary-700">{animal}</span>
            <span className="font-bold text-primary-900">{count}</span>
          </div>
        ))}
      </div>
      <button
        onClick={() => handleGameLoad(game.id)}
        className="text-white px-4 py-2 cursor-pointer rounded-md transition duration-300 text-center bg-blue-500 hover:bg-blue-300"
      >
        Load
      </button>
    </div>
  );
};

LoadGameInfo.propTypes = {
  game: PropTypes.object.isRequired,
};

export default LoadGameInfo;
