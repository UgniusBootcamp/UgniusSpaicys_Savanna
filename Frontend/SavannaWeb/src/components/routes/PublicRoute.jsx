import { Navigate } from 'react-router';
import routes from '../../constants/routes';
import { useAuth } from '../../hooks/useAuth';
import { Spinner } from '../common/Spinner';

const PublicRoute = ({ element }) => {
  const { isLoggedIn, isLoading } = useAuth();

  if (isLoading) {
    return <Spinner />;
  }

  return isLoggedIn ? <Navigate to={routes.savanna} /> : element;
};

export default PublicRoute;
