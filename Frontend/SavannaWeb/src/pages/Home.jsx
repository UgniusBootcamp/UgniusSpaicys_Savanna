import { faPaw, faSun } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useNavigate } from 'react-router';
import Button from '../components/common/Button';
import routes from '../constants/routes';

const Home = () => {
  const navigate = useNavigate();

  return (
    <div className="flex items-center justify-center h-screen bg-cover bg-center">
      <div className="text-center text-white p-8 shadow-lg bg-primary-700 bg-opacity-50 rounded-lg">
        <h1 className="text-4xl font-bold mb-4">Welcome to Savanna</h1>
        <div className="flex justify-center mb-4">
          <FontAwesomeIcon icon={faPaw} className="text-5xl mx-2" />
          <FontAwesomeIcon icon={faSun} className="text-5xl mx-2" />
        </div>
        <p className="text-lg mb-6">Explore the wild beauty of the Savanna</p>
        <Button onClick={() => navigate(routes.login)} className="text-lg ">
          Explore Now
        </Button>
      </div>
    </div>
  );
};

export default Home;
