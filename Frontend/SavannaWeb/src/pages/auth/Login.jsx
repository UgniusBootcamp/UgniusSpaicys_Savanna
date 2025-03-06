import { useState } from 'react';
import { useNavigate } from 'react-router';
import Button from '../../components/common/Button';
import FormTemplate from '../../components/form/FormTemplate';
import routes from '../../constants/routes';
import { useAuth } from '../../hooks/useAuth';

const Login = () => {
  const navigate = useNavigate();
  const { login } = useAuth();
  const [error, setError] = useState(null);

  const [formData, setFormData] = useState({
    userName: '',
    password: '',
  });

  const handleChanges = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const loginData = {
        userName: formData.userName,
        password: formData.password,
      };

      await login(loginData);
      navigate(routes.home);
    } catch (error) {
      setError('Invalid username or password');
    }
  };

  return (
    <FormTemplate header={'Welcome back to Savanna'}>
      <form onSubmit={handleSubmit}>
        <div className="w-full mb-4">
          <label
            htmlFor="userName"
            className="block text-sm font-medium text-gray-700"
          >
            Username
          </label>
          <input
            id="userName"
            name="userName"
            onChange={handleChanges}
            value={formData.userName}
            autoComplete={'userName'}
            type="text"
            required
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
            name="password"
            onChange={handleChanges}
            type="password"
            required
            autoComplete={'current-password'}
            className="mt-1 p-2 w-full border border-primary-300 rounded-lg shadow-sm focus:outline-none focus:ring-1 focus:ring-primary-500 focus:border-primary-500 transition duration-200"
            placeholder="Confirm your password"
          />
          <div className="my-2 text-red-500 ">
            {error && <span>{error}</span>}
          </div>
        </div>

        <Button className="w-full bg-primary-900 hover:bg-primary-700 mb-4">
          Log In to Savanna
        </Button>
        <div className="flex justify-center items-center mb-">
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
      </form>
    </FormTemplate>
  );
};

export default Login;
