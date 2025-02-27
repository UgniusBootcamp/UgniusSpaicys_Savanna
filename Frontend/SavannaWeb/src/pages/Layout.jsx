import { Outlet } from 'react-router';
import Footer from '../components/layout/Footer';
import Header from '../components/layout/Header';

const Layout = () => {
  return (
    <div className="flex flex-col min-h-screen font-roboto">
      <Header />
      <main className="flex flex-grow container mx-auto">
        <Outlet />
      </main>
      <Footer />
    </div>
  );
};

export default Layout;
