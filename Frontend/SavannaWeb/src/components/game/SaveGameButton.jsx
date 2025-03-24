import { useState } from 'react';
import SignalRService from '../../api/SignalRService';
import gameActionsConstants from '../../constants/gameActionsConstants';
import { useAuth } from '../../hooks/useAuth';
import ConfirmationModal from '../common/ConfirmationModal';

const SaveGameButton = () => {
  const [showConfirmation, setShowConfirmation] = useState(false);
  const { setSnackbarMessage } = useAuth();

  const renderConfirmation = () => {
    if (!showConfirmation) return;

    return (
      <ConfirmationModal
        header={'Save Game'}
        message={
          'Are you sure you want to save game? Previous progress of this game will be overwritten.'
        }
        isOpen={showConfirmation}
        onClose={() => setShowConfirmation(false)}
        onConfirm={() => {
          setShowConfirmation(false);
          SignalRService.saveGame();
          setSnackbarMessage('Game has been saved.');
        }}
        onConfirmMessage={'Save'}
      />
    );
  };

  const baseClass =
    'text-white px-4 py-2 cursor-pointer rounded-md transition duration-300 hover:-translate-y-1 text-center';

  return (
    <div>
      <div
        onClick={() => setShowConfirmation(true)}
        className={`${baseClass} bg-blue-500 hover:bg-blue-300`}
      >
        {gameActionsConstants.save}
      </div>
      {renderConfirmation()}
    </div>
  );
};

export default SaveGameButton;
