import { useEffect, useState } from 'react';
import AccountApi from '../../api/AccountApi';

const Savanna = () => {
  const [test, setTest] = useState(null);

  useEffect(() => {
    const fetchData = () => {
      const response = AccountApi.test();
      setTest(response.data);
      console.log(response.data);
    };

    fetchData();
  }, []);

  return <div>Savanna</div>;
};

export default Savanna;
