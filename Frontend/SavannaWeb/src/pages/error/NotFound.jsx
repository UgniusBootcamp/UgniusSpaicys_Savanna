import { useNavigate } from 'react-router';
import Button from '../../components/common/Button';
import routes from '../../constants/routes';

const NotFound = () => {
  const navigate = useNavigate();

  const goHome = () => {
    navigate(routes.home);
  };

  return (
    <div className="flex flex-col items-center justify-center min-h-screen text-center">
      <h1 className="text-9xl font-extrabold text-primary-800 animate-bounce">
        404
      </h1>
      <h2 className="text-2xl text-primary-700 mt-4">Page was not found</h2>
      <p className="mt-2 font-semibold text-primary-500">
        Sorry, the page you are looking for does not exist.
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

export default NotFound;
