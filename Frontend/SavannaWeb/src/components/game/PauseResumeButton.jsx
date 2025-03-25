import PropTypes from 'prop-types';
import SignalRService from '../../api/SignalRService';
import gameActionsConstants from '../../constants/gameActionsConstants';

const PauseResumeButton = ({ isRunning }) => {
  const baseClass =
    'text-white px-4 py-2 cursor-pointer rounded-md transition duration-300 hover:-translate-y-1';

  return (
    <div>
      {isRunning ? (
        <div
          className={`${baseClass} bg-rose-600 bg-hover-rose-400`}
          onClick={() => SignalRService.pauseGame()}
        >
          {gameActionsConstants.pause}
        </div>
      ) : (
        <div
          className={`${baseClass} bg-green-500 hover:bg-green-300`}
          onClick={() => SignalRService.resumeGame()}
        >
          {gameActionsConstants.resume}
        </div>
      )}
    </div>
  );
};

PauseResumeButton.propTypes = {
  isRunning: PropTypes.bool.isRequired,
};

export default PauseResumeButton;
