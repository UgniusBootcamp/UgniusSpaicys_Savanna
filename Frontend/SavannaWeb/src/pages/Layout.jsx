import { Outlet } from 'react-router';
import savannaImage from '../assets/savanna.jpg';
import SnackbarHandler from '../components/common/SnackbarHandler';
import Footer from '../components/layout/Footer';
import Header from '../components/layout/Header';

const Layout = () => {
  return (
    <div
      className="flex flex-col min-h-screen font-roboto"
      style={{
        backgroundImage: `url(${savannaImage})`,
        backgroundSize: 'cover',
        backgroundPosition: 'center',
      }}
    >
      <Header />
      <main className="flex flex-grow justify-center items-center container mx-auto px-6 py-8 bg-primary-100/80 m-4 rounded-lg ">
        <Outlet />
      </main>
      <Footer />
      <SnackbarHandler />
    </div>
  );
};

export default Layout;
