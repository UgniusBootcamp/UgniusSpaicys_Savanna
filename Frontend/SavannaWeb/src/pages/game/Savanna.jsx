import { useEffect, useState } from 'react';
import AccountApi from '../../api/AccountApi';

const Savanna = () => {
  const [test, setTest] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      const response = await AccountApi.test();
      setTest(response.data);
      console.log(response.data);
    };

    fetchData();
  }, []);

  return (
    <div>
      <h1>Savanna</h1>
    </div>
  );
};

export default Savanna;
