import { useState } from 'react';
import { useNavigate } from 'react-router';
import AccountApi from '../../api/AccountApi';
import Button from '../../components/common/Button';
import { Spinner } from '../../components/common/Spinner';
import FormTemplate from '../../components/form/FormTemplate';
import routes from '../../constants/routes';
import useErrorHandler from '../../hooks/useErrorHandler';

const Register = () => {
  const navigate = useNavigate();
  const errorHandler = useErrorHandler();
  const [isLoading, setIsLoading] = useState(false);
  const [formData, setFormData] = useState({
    userName: '',
    password: '',
    confirmPassword: '',
  });

  const [errors, setErrors] = useState({});

  const validate = () => {
    let newErrors = {};
    if (!formData.userName) newErrors.userName = 'Username is required';
    if (!formData.password) {
      newErrors.password = 'Password is required';
    } else if (
      formData.password.length < 8 ||
      !/(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d])/.test(formData.password)
    ) {
      newErrors.password =
        'Password must be at least 8 characters long and include one uppercase letter, one lowercase letter, one number, and one symbol';
    }

    if (formData.confirmPassword !== formData.password) {
      newErrors.confirmPassword = 'Passwords do not match';
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
      setIsLoading(true);
      try {
        const userData = {
          userName: formData.userName,
          password: formData.password,
          confirmPassword: formData.confirmPassword,
        };

        const response = await AccountApi.register(userData);

        if (response.success) navigate(routes.home);
      } catch (error) {
        error.status === 422
          ? setErrors({ usernameExist: 'Username already exists' })
          : errorHandler.handleError(error);
      } finally {
        setIsLoading(false);
      }
    }
  };

  if (isLoading) {
    return <Spinner />;
  }

  return (
    <FormTemplate header={'Join the Savanna Adventure'}>
      <form onSubmit={handleSumbit}>
        <div className="w-full mb-4">
          {errors.usernameExist && (
            <p className="text-red-500 text-sm">{errors.usernameExist}</p>
          )}
          <label
            htmlFor="userName"
            className="block text-sm font-medium text-gray-700"
          >
            Username
          </label>
          <input
            id="userName"
            name="userName"
            type="text"
            value={formData.userName}
            maxLength={255}
            onChange={handleChange}
            className="mt-1 p-2 w-full border border-primary-300 rounded-lg shadow-sm focus:outline-none focus:ring-1 focus:ring-primary-500 focus:border-primary-500 transition duration-200"
            placeholder="Enter your username"
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
            Password
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
            <span>Password must include:</span>
            <ul className="list-disc list-inside">
              <li>At least 8 characters</li>
              <li>One uppercase letter</li>
              <li>One lowercase letter</li>
              <li>One number</li>
              <li>One symbol</li>
            </ul>
          </div>
        </div>

        <div className="w-full mb-6">
          <label
            htmlFor="confirmPassword"
            className="block text-sm font-medium text-gray-700"
          >
            Confirm Password
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
          Join Savanna
        </Button>

        <div className="flex justify-center items-center mb-4">
          <p className="text-base">
            Already a member of Savanna?{' '}
            <a
              className="underline text-primary-900 font-semibold hover:text-primary-700 cursor-pointer transition duration-300"
              onClick={() => navigate(routes.login)}
            >
              Log in
            </a>
          </p>
        </div>
      </form>
    </FormTemplate>
  );
};

export default Register;
