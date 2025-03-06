import { useMemo } from 'react';
import { useNavigate } from 'react-router';
import ErrorHandler from '../components/common/ErrorHandler';

const useErrorHandler = () => {
  const navigate = useNavigate();
  return useMemo(() => new ErrorHandler(navigate), [navigate]);
};

export default useErrorHandler;
