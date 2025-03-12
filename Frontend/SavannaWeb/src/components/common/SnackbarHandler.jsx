import { useEffect, useState } from 'react';
import { useAuth } from '../../hooks/useAuth';
import MySnackbar from './MySnackbar';

const SnackbarHandler = () => {
  const { snackbarMessage, setSnackbarMessage } = useAuth();
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    if (snackbarMessage) {
      setIsModalOpen(true);
    }
  }, [snackbarMessage]);

  const handleClose = () => {
    setIsModalOpen(false);
    setSnackbarMessage('');
  };

  return (
    <MySnackbar
      isOpen={isModalOpen}
      message={snackbarMessage}
      onClose={handleClose}
      severity="success"
    />
  );
};

export default SnackbarHandler;
