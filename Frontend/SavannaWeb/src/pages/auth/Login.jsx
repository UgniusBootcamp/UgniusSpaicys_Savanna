import { useNavigate } from 'react-router';
import Button from '../../components/common/Button';
import routes from '../../constants/routes';

const Login = () => {
  const navigate = useNavigate();

  return (
    <div className="w-full flex justify-center items-center">
      <div className="w-full sm:w-96 border-2 rounded-lg bg-primary-50 border-primary-300 p-6 flex flex-col items-center">
        <h2 className="text-2xl font-semibold text-gray-800 mb-6">
          Welcome Back to Savanna
        </h2>

        <div className="w-full mb-4">
          <label
            htmlFor="username"
            className="block text-sm font-medium text-gray-700"
          >
            Username
          </label>
          <input
            id="username"
            type="text"
            className="mt-1 p-2 w-full border border-primary-300 rounded-lg shadow-sm focus:outline-none focus:ring-1 focus:ring-primary-500 focus:border-primary-500 transition duration-200"
            placeholder="Enter your username"
          />
        </div>

        <div className="w-full mb-6">
          <label
            htmlFor="password"
            className="block text-sm font-medium text-gray-700"
          >
            Password
          </label>
          <input
            id="password"
            type="password"
            className="mt-1 p-2 w-full border border-primary-300 rounded-lg shadow-sm focus:outline-none focus:ring-1 focus:ring-primary-500 focus:border-primary-500 transition duration-200"
            placeholder="Confirm your password"
          />
        </div>

        <Button className="w-full bg-primary-900 hover:bg-primary-700 mb-4">
          Log In to Savanna
        </Button>
        <div className="flex justify-end items-center mb-4">
          <p className="text-base">
            New to Savanna?{' '}
            <a
              className="underline text-primary-900 font-semibold hover:text-primary-700 cursor-pointer transition duration-300"
              onClick={() => navigate(routes.register)}
            >
              Join the Adventure
            </a>
          </p>
        </div>
      </div>
    </div>
  );
};

export default Login;
