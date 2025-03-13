import PropTypes from 'prop-types';
import { Spinner } from '../common/Spinner';

const GameStats = ({ iteration, animalsCount }) => {
  if (!iteration) return <Spinner />;

  return (
    <div className="bg-primary-400 rounded-md flex flex-col justify-center items-center h-full w-full p-4">
      <div className="text-lg font-bold">Iteration {iteration}</div>
      <div className="mt-2 flex flex-wrap justify-between items-center w-full gap-x-2">
        {Object.entries(animalsCount).map(([animal, count], index) => (
          <div key={index} className="text-sm flex flex-col text-center">
            <p>{animal}</p>
            <p>{count}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default GameStats;

GameStats.propTypes = {
  iteration: PropTypes.number.isRequired,
  animalsCount: PropTypes.object.isRequired,
};
