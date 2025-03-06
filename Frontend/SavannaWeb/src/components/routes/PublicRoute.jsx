import { Navigate } from 'react-router';
import routes from '../../constants/routes';
import { useAuth } from '../../hooks/useAuth';

const PublicRoute = ({ element }) => {
  const { isLoggedIn } = useAuth();

  return isLoggedIn ? <Navigate to={routes.savanna} /> : element;
};

export default PublicRoute;
