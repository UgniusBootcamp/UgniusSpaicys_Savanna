import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import Slide from '@mui/material/Slide';
import PropTypes from 'prop-types';
import * as React from 'react';

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="down" ref={ref} {...props} />;
});

export default function ConfirmationModal({
  header,
  message,
  isOpen,
  onClose,
  onConfirm,
  onCloseMessage = 'Cancel',
  onConfirmMessage = 'Confirm',
}) {
  return (
    <Dialog
      open={isOpen}
      slots={{
        transition: Transition,
      }}
      keepMounted
      onClose={onClose}
      aria-describedby="alert-dialog-slide-description"
      sx={{
        '& .MuiPaper-root': {
          borderRadius: 3,
          p: 2,
          boxShadow: 6,
          minWidth: 360,
          maxWidth: 500,
          backgroundColor: '#e8e0b8',
        },
      }}
    >
      <DialogTitle
        sx={{
          fontWeight: 600,
          fontSize: '1.25rem',
          color: '#1e1308',
          mb: 1,
        }}
      >
        {header}
      </DialogTitle>
      <DialogContent>
        <DialogContentText
          id="alert-dialog-slide-description"
          sx={{
            fontSize: '1rem',
            color: '#1e1308',
          }}
        >
          {message}
        </DialogContentText>
      </DialogContent>
      <DialogActions
        sx={{
          px: 3,
          pb: 2,
          display: 'flex',
          justifyContent: 'flex-end',
          gap: 1,
        }}
      >
        <Button
          variant="contained"
          color="inherit"
          onClick={onClose}
          sx={{ textTransform: 'none' }}
        >
          {onCloseMessage}
        </Button>
        <Button
          variant="contained"
          color="primary"
          onClick={onConfirm}
          sx={{ textTransform: 'none' }}
        >
          {onConfirmMessage}
        </Button>
      </DialogActions>
    </Dialog>
  );
}

ConfirmationModal.propTypes = {
  header: PropTypes.string.isRequired,
  message: PropTypes.string.isRequired,
  isOpen: PropTypes.bool.isRequired,
  onClose: PropTypes.func.isRequired,
  onConfirm: PropTypes.func.isRequired,
  onCloseMessage: PropTypes.string,
  onConfirmMessage: PropTypes.string,
};
