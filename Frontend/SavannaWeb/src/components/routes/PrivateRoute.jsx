import { Navigate } from 'react-router';
import routes from '../../constants/routes';
import { useAuth } from '../../hooks/useAuth';

const PrivateRoute = ({ element }) => {
  const { isLoggedIn } = useAuth();

  return isLoggedIn ? element : <Navigate to={routes.login} />;
};

export default PrivateRoute;
