import { useNavigate } from 'react-router';
import Button from '../../components/common/Button';
import errorConstants from '../../constants/errorConstants';
import routes from '../../constants/routes';

const Error = () => {
  const navigate = useNavigate();

  const goHome = () => {
    navigate(routes.home);
  };

  return (
    <div className="flex flex-col items-center justify-center min-h-screen text-center">
      <div className="max-w-lg">
        <h1 className="text-9xl font-bold text-primary-800 animate-bounce">
          500
        </h1>
        <h2 className="text-4xl mt-4 text-primary-700 font-bold">
          {errorConstants.somethingWentWrong}
        </h2>
        <p>{errorConstants.message}</p>
        <Button
          onClick={goHome}
          className="bg-primary-700 mt-6 text-white hover:bg-primary-400"
        >
          {errorConstants.goToHome}
        </Button>
      </div>
    </div>
  );
};

export default Error;
