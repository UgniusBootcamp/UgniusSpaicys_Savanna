import footerConstants from '../../constants/footerConstants';

const Footer = () => {
  return (
    <footer className="w-full min-h-16 bg-primary-700 flex items-center justify-center px-6">
      <p className="text-white">
        &copy; {new Date().getFullYear()} {footerConstants.message}
      </p>
    </footer>
  );
};

export default Footer;
