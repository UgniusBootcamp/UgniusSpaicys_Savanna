import React from 'react';

const Button = ({ children, className, ...props }) => {
  return (
    <button
      className={`bg-primary-500 text-white py-2 px-4 rounded-lg hover:bg-primary-400 transition duration-300 cursor-pointer ${className}`}
      {...props}
    >
      {children}
    </button>
  );
};

export default Button;
