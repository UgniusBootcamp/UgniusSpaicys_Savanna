import PropTypes from 'prop-types';
import { Spinner } from '../common/Spinner';

const GameStats = ({ iteration, animalsCount }) => {
  if (!iteration) return <Spinner />;

  return (
    <div className="text-primary-800 flex flex-col justify-center items-center h-full w-full p-4">
      <div className="text-lg font-bold">Iteration {iteration}</div>
      <div className="mt-2 flex flex-wrap justify-between items-center w-full gap-2">
        {Object.entries(animalsCount).map(([animal, count], index) => (
          <div
            key={index}
            className="text-sm flex flex-col text-center text-white bg-primary-500 px-2 py-0.5 rounded"
          >
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
