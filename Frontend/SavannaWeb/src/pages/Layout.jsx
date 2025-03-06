import { Outlet } from 'react-router';
import SnackbarHandler from '../components/common/SnackbarHandler';
import Footer from '../components/layout/Footer';
import Header from '../components/layout/Header';

const Layout = () => {
  return (
    <div className="flex flex-col min-h-screen font-roboto  bg-primary-200">
      <Header />
      <main className="flex flex-grow justify-center items-center container mx-auto px-6 py-8">
        <Outlet />
      </main>
      <Footer />
      <SnackbarHandler />
    </div>
  );
};

export default Layout;
