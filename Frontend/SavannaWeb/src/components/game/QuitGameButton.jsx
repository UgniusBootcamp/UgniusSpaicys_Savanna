import PropTypes from 'prop-types';
import { useState } from 'react';
import SignalRService from '../../api/SignalRService';
import gameActionsConstants from '../../constants/gameActionsConstants';
import ConfirmationModal from '../common/ConfirmationModal';

const QuitGameButton = ({ onQuit }) => {
  const [showConfirmation, setShowConfirmation] = useState(false);

  const renderConfirmation = () => {
    if (!showConfirmation) return;

    return (
      <ConfirmationModal
        header={'Quit Game'}
        message={
          'Are you sure you want to quit game? Progress will not be saved automatically.'
        }
        isOpen={showConfirmation}
        onClose={() => setShowConfirmation(false)}
        onConfirm={() => {
          setShowConfirmation(false);
          SignalRService.quitGame();
          onQuit();
        }}
        onConfirmMessage={'Quit'}
      />
    );
  };

  const baseClass =
    'text-white px-4 py-2 cursor-pointer rounded-md transition duration-300 hover:-translate-y-1 text-center';

  return (
    <div>
      <div
        onClick={() => setShowConfirmation(true)}
        className={`${baseClass} bg-red-950 hover:bg-red-800`}
      >
        {gameActionsConstants.quit}
      </div>
      {renderConfirmation()}
    </div>
  );
};

QuitGameButton.propTypes = {
  onQuit: PropTypes.func.isRequired,
};

export default QuitGameButton;
