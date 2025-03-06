import { faSkullCrossbones } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useNavigate } from 'react-router';
import Button from '../../components/common/Button';
import routes from '../../constants/routes';

const NoAccess = () => {
  const navigate = useNavigate();

  const goHome = () => {
    navigate(routes.home);
  };

  return (
    <div className="flex flex-col items-center justify-center min-h-screen text-center">
      <div>
        <FontAwesomeIcon
          icon={faSkullCrossbones}
          className="text-6xl text-primary-800 animate-bounce"
        />
      </div>
      <h1 className="text-4xl font-bold text-primary-800">No Access</h1>
      <p className="mt-4 text-primary-500 text-center max-w-md font-semibold">
        Sorry, you do not have the necessary permissions to access this page.
      </p>
      <Button
        onClick={goHome}
        className="bg-primary-700 mt-6 text-white hover:bg-primary-400"
      >
        Go to Home
      </Button>
    </div>
  );
};

export default NoAccess;
