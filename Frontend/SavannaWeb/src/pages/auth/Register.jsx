import { useState } from 'react';
import { useNavigate } from 'react-router';
import AccountApi from '../../api/AccountApi';
import Button from '../../components/common/Button';
import FormTemplate from '../../components/form/FormTemplate';
import loginConstants from '../../constants/loginConstants';
import registerConstants from '../../constants/registerConstants';
import routes from '../../constants/routes';
import { useAuth } from '../../hooks/useAuth';
import useErrorHandler from '../../hooks/useErrorHandler';

const Register = () => {
  const navigate = useNavigate();
  const errorHandler = useErrorHandler();
  const { setSnackbarMessage } = useAuth();
  const [formData, setFormData] = useState({
    userName: '',
    password: '',
    confirmPassword: '',
  });

  const [errors, setErrors] = useState({});

  const validate = () => {
    let newErrors = {};
    if (!formData.userName)
      newErrors.userName = registerConstants.usernameRequired;
    if (!formData.password) {
      newErrors.password = registerConstants.passwordRequired;
    } else if (
      formData.password.length < 8 ||
      !/(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d])/.test(formData.password)
    ) {
      newErrors.password = registerConstants.passwordRegulation;
    }

    if (formData.confirmPassword !== formData.password) {
      newErrors.confirmPassword = registerConstants.passwordMatch;
    }

    setErrors(newErrors);

    return Object.keys(newErrors).length === 0;
  };
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSumbit = async (e) => {
    e.preventDefault();

    if (validate()) {
      try {
        const userData = {
          userName: formData.userName,
          password: formData.password,
          confirmPassword: formData.confirmPassword,
        };

        const response = await AccountApi.register(userData);

        if (response.success) {
          setSnackbarMessage(
            `${registerConstants.welcomeToSavanna} ${formData.userName}`,
          );
          navigate(routes.home);
        }
      } catch (error) {
        error.status === 422
          ? setErrors({ usernameExist: registerConstants.usernameExists })
          : errorHandler.handleError(error);
      } finally {
      }
    }
  };

  return (
    <FormTemplate header={registerConstants.joinTheSavannaAdventure}>
      <form onSubmit={handleSumbit}>
        <div className="w-full mb-4">
          {errors.usernameExist && (
            <p className="text-red-500 text-sm">{errors.usernameExist}</p>
          )}
          <label
            htmlFor="userName"
            className="block text-sm font-medium text-gray-700"
          >
            {loginConstants.username}
          </label>
          <input
            id="userName"
            name="userName"
            type="text"
            value={formData.userName}
            maxLength={255}
            onChange={handleChange}
            className="mt-1 p-2 w-full border border-primary-300 rounded-lg shadow-sm focus:outline-none focus:ring-1 focus:ring-primary-500 focus:border-primary-500 transition duration-200"
            placeholder={loginConstants.usernamePlaceholder}
          />
          {errors.userName && (
            <p className="text-red-500 text-sm">{errors.userName}</p>
          )}
        </div>

        <div className="w-full mb-4">
          <label
            htmlFor="password"
            className="block text-sm font-medium text-gray-700"
          >
            {loginConstants.password}
          </label>
          <input
            id="password"
            type="password"
            autoComplete="new-password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            maxLength={100}
            className="mt-1 p-2 w-full border border-primary-300 rounded-lg shadow-sm focus:outline-none focus:ring-1 focus:ring-primary-500 focus:border-primary-500 transition duration-200"
            placeholder="Enter your password"
          />
          <div
            className={`text-sm mt-1 text-primary-500 ${
              errors.password ? 'text-red-500' : ''
            }`}
          >
            <span>{registerConstants.passwordMustInclude}</span>
            <ul className="list-disc list-inside">
              <li>{registerConstants.atLeast8Chars}</li>
              <li>{registerConstants.oneUppercase}</li>
              <li>{registerConstants.oneLowercase}</li>
              <li>{registerConstants.oneNumber}</li>
              <li>{registerConstants.oneSymbol}</li>
            </ul>
          </div>
        </div>

        <div className="w-full mb-6">
          <label
            htmlFor="confirmPassword"
            className="block text-sm font-medium text-gray-700"
          >
            {loginConstants.confirmPassword}
          </label>
          <input
            id="confirmPassword"
            name="confirmPassword"
            value={formData.confirmPassword}
            onChange={handleChange}
            maxLength={100}
            type="password"
            className="mt-1 p-2 w-full border border-primary-300 rounded-lg shadow-sm focus:outline-none focus:ring-1 focus:ring-primary-500 focus:border-primary-500 transition duration-200"
            placeholder="Enter your password"
          />
          {errors.confirmPassword && (
            <p className="text-red-500 text-sm">{errors.confirmPassword}</p>
          )}
        </div>

        <Button className="w-full bg-primary-900 hover:bg-primary-700 mb-4">
          {registerConstants.joinSavanna}
        </Button>

        <div className="flex justify-center items-center mb-4">
          <p className="text-base">
            {registerConstants.alreadyMemeber}{' '}
            <a
              className="underline text-primary-900 font-semibold hover:text-primary-700 cursor-pointer transition duration-300"
              onClick={() => navigate(routes.login)}
            >
              {registerConstants.login}
            </a>
          </p>
        </div>
      </form>
    </FormTemplate>
  );
};

export default Register;
