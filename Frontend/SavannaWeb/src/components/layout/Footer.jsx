import React from 'react';

const Footer = () => {
  return (
    <footer className="w-full h-16 bg-primary-700 flex items-center justify-center px-6">
      <p className="text-white">
        &copy; {new Date().getFullYear()} Savanna App. All rights reserved.
      </p>
    </footer>
  );
};

export default Footer;
