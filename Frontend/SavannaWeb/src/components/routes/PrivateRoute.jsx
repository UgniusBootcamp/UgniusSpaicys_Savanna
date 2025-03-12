import { Navigate, useLocation } from 'react-router';
import routes from '../../constants/routes';
import { useAuth } from '../../hooks/useAuth';
import { Spinner } from '../common/Spinner';

const PrivateRoute = ({ element }) => {
  const { isLoggedIn, isLoading } = useAuth();
  const location = useLocation();

  if (isLoading) {
    return <Spinner />;
  }

  return isLoggedIn ? (
    element
  ) : (
    <Navigate to={routes.login} state={{ from: location }} />
  );
};

export default PrivateRoute;
