import { useNavigate } from 'react-router';
import routes from '../../constants/routes';
import Button from '../common/Button';

const Header = () => {
  const navigate = useNavigate();

  return (
    <header className="w-full h-16 bg-primary-700 flex items-center gap-x-2 px-6">
      <div className="flex justify-between items-center w-full">
        <div>
          <h1 className="text-2xl text-white">Savanna App</h1>
        </div>
        <Button onClick={() => navigate(routes.login)}>Login</Button>
      </div>
    </header>
  );
};
export default Header;
