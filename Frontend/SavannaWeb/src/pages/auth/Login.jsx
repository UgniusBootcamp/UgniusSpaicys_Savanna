import { useState } from 'react';
import { useNavigate } from 'react-router';
import Button from '../../components/common/Button';
import FormTemplate from '../../components/form/FormTemplate';
import loginConstants from '../../constants/loginConstants';
import routes from '../../constants/routes';
import { useAuth } from '../../hooks/useAuth';

const Login = () => {
  const navigate = useNavigate();
  const { login, setSnackbarMessage } = useAuth();
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
      setSnackbarMessage(loginConstants.loginSuccessful);
      navigate(routes.home);
    } catch (error) {
      setError(loginConstants.invalidUserCredentials);
    }
  };

  return (
    <FormTemplate header={loginConstants.welcomeBackToSavanna}>
      <form onSubmit={handleSubmit}>
        <div className="w-full mb-4">
          <label
            htmlFor="userName"
            className="block text-sm font-medium text-primary-950"
          >
            {loginConstants.username}
          </label>
          <input
            id="userName"
            name="userName"
            onChange={handleChanges}
            value={formData.userName}
            autoComplete={'userName'}
            type="text"
            required
            className="mt-1 p-2 w-full bg-primary-100 border border-primary-200 rounded-lg shadow-sm focus:outline-none focus:ring-1 focus:ring-primary-100 focus:border-primary-100 transition duration-200"
            placeholder={loginConstants.usernamePlaceholder}
          />
        </div>

        <div className="w-full mb-6">
          <label
            htmlFor="password"
            className="block text-sm font-medium text-primary-950"
          >
            {loginConstants.password}
          </label>
          <input
            id="password"
            name="password"
            onChange={handleChanges}
            type="password"
            required
            autoComplete={'current-password'}
            className="mt-1 p-2 w-full bg-primary-100 border border-primary-200 rounded-lg shadow-sm focus:outline-none focus:ring-1 focus:ring-primary-100 focus:border-primary-100 transition duration-200"
            placeholder={loginConstants.passwordConfirm}
          />
          <div className="my-2 text-red-600 ">
            {error && <span>{error}</span>}
          </div>
        </div>

        <Button className="w-full bg-primary-900 hover:bg-primary-700 mb-4">
          {loginConstants.loginToSavanna}
        </Button>
        <div className="flex justify-center items-center mb-">
          <p className="text-base">
            {loginConstants.newToSavanna}{' '}
            <a
              className="underline text-primary-900 font-semibold hover:text-primary-700 cursor-pointer transition duration-300"
              onClick={() => navigate(routes.register)}
            >
              {loginConstants.joinTheAdventure}
            </a>
          </p>
        </div>
      </form>
    </FormTemplate>
  );
};

export default Login;
