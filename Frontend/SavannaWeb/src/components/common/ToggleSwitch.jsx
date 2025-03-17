import FormControlLabel from '@mui/material/FormControlLabel';
import MuiSwitch from '@mui/material/Switch';
import PropTypes from 'prop-types';

const ToggleSwitch = ({ checked, onChange, label }) => {
  return (
    <FormControlLabel
      control={<MuiSwitch checked={checked} onChange={onChange} />}
      label={<span style={{ fontSize: '1.5rem' }}>{label}</span>}
    />
  );
};

ToggleSwitch.propTypes = {
  checked: PropTypes.bool.isRequired,
  onChange: PropTypes.func.isRequired,
  label: PropTypes.string.isRequired,
};

export default ToggleSwitch;
