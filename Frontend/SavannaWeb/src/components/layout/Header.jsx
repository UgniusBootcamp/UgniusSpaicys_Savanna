import { useNavigate } from 'react-router';
import headerConstants from '../../constants/headerConstants';
import routes from '../../constants/routes';
import { useAuth } from '../../hooks/useAuth';
import Button from '../common/Button';

const Header = () => {
  const navigate = useNavigate();
  const { isLoggedIn, userName, logOut, isLoading } = useAuth();

  return (
    <header className="w-full h-16 bg-primary-700 flex items-center gap-x-2 px-6">
      <div className="flex justify-between items-center w-full">
        <div>
          <h1 className="text-2xl text-white">{headerConstants.savannaApp}</h1>
        </div>
        {!isLoading && (
          <div className="flex justify-between items-center text-white">
            {isLoggedIn && (
              <div className="text-sm mr-2">
                {headerConstants.welcome} {userName}
              </div>
            )}
            <div>
              {isLoggedIn ? (
                <Button onClick={logOut}>{headerConstants.logout}</Button>
              ) : (
                <Button onClick={() => navigate(routes.login)}>Login</Button>
              )}
            </div>
          </div>
        )}
      </div>
    </header>
  );
};
export default Header;
