import Alert from '@mui/material/Alert';
import Snackbar from '@mui/material/Snackbar';
import PropTypes from 'prop-types';

const AnimalInfoSnackbar = ({ selectedAnimal, onClose }) => {
  const open = Boolean(selectedAnimal);

  return (
    <Snackbar
      open={open}
      onClose={(_, reason) => {
        if (reason === 'clickaway') return;
        onClose();
      }}
      anchorOrigin={{ vertical: 'bottom', horizontal: 'left' }}
    >
      <Alert onClose={onClose} severity="info" sx={{ width: '100%' }}>
        {selectedAnimal && (
          <>
            <strong>Species:</strong> {selectedAnimal.species} <br />
            <strong>Age:</strong> {selectedAnimal.features.age} <br />
            <strong>Health:</strong> {selectedAnimal.features.health} <br />
            <strong>Offsprings:</strong> {selectedAnimal.features.offsprings}
          </>
        )}
      </Alert>
    </Snackbar>
  );
};

AnimalInfoSnackbar.propTypes = {
  selectedAnimal: PropTypes.object,
  onClose: PropTypes.func.isRequired,
};

export default AnimalInfoSnackbar;
