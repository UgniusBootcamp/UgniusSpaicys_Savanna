import { Alert, Snackbar } from '@mui/material';
import PropTypes from 'prop-types';

const MySnackbar = ({
  isOpen,
  message,
  onClose,
  severity,
  duration = 2000,
}) => {
  return (
    <div className="absolute">
      <div className="fixed bottom-4 left-4 z-50">
        <Snackbar
          open={isOpen}
          autoHideDuration={duration}
          onClose={onClose}
          anchorOrigin={{ vertical: 'bottom', horizontal: 'left' }}
          sx={{ width: '20rem' }}
        >
          <Alert
            onClose={onClose}
            severity={severity}
            variant="filled"
            sx={{
              width: '100%',
              fontSize: '1.2rem',
              padding: '1rem',
            }}
          >
            {message}
          </Alert>
        </Snackbar>
      </div>
    </div>
  );
};

MySnackbar.propTypes = {
  isOpen: PropTypes.bool.isRequired,
  message: PropTypes.string.isRequired,
  onClose: PropTypes.func.isRequired,
  severity: PropTypes.oneOf(['error', 'info', 'success', 'warning']),
  duration: PropTypes.number,
};

export default MySnackbar;
