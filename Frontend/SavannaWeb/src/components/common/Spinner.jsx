import PropTypes from 'prop-types';
import { useEffect, useState } from 'react';

export const Spinner = ({ delay = 500 }) => {
  const [show, setShow] = useState(false);

  useEffect(() => {
    const timer = setTimeout(() => setShow(true), delay);

    return () => clearTimeout(timer);
  }, [delay]);

  if (!show) return null;

  return (
    <div className="flex justify-center items-center min-h-screen">
      <div
        className="text-primary-500 inline-block h-14 w-14 animate-spin rounded-full border-4 border-solid border-current border-r-transparent align-[-0.125em] motion-reduce:animate-[spin_1.5s_linear-infinite]"
        role="status"
      >
        <span className="!absolute !-m-px !h-px !w-px !overflow-hidden !whitespace-nowrap !border-0 !p-0 ![clip:rect(0 0 0 0)]">
          Loading...
        </span>
      </div>
    </div>
  );
};

Spinner.propTypes = {
  delay: PropTypes.number,
};
