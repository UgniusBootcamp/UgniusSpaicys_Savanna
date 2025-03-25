import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { format } from 'date-fns';
import { useEffect, useState } from 'react';
import GameApi from '../../api/GameApi';
import gameCreationConstants from '../../constants/gameCreationConstants';
import useErrorHandler from '../../hooks/useErrorHandler';
import { Spinner } from '../common/Spinner';
import LoadGameInfo from './LoadGameInfo';

const LoadGame = () => {
  const [games, setGames] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const errorHandler = useErrorHandler();

  const [startDate, setStartDate] = useState(() => {
    const date = new Date();
    date.setDate(date.getDate() - 3);
    return date;
  });
  const [endDate, setEndDate] = useState(new Date());

  const fetchGames = async () => {
    if (!startDate || !endDate) return;

    setIsLoading(true);
    try {
      const startStr = format(startDate, 'yyyy-MM-dd');
      const endStr = format(endDate, 'yyyy-MM-dd');

      const gamesResponse = await GameApi.getUserGames(startStr, endStr);
      setGames(gamesResponse.data);
    } catch (err) {
      errorHandler.handleError(err);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    if (startDate && endDate) {
      fetchGames();
    }
  }, [startDate, endDate]);

  return (
    <div className="w-full flex justify-center items-center">
      <div className="w-full min-h-[50vh] rounded-lg bg-primary-300 p-6 flex flex-col items-center">
        <LocalizationProvider dateAdapter={AdapterDateFns}>
          <div className="flex flex-col gap-4">
            <div className="flex flex-wrap gap-4 items-center scale-75">
              <DatePicker
                label={gameCreationConstants.startDate}
                value={startDate}
                onChange={(newValue) => setStartDate(newValue)}
                slotProps={{
                  textField: {
                    size: 'small',
                    className: 'bg-white rounded-md shadow-sm w-full ',
                    InputProps: {
                      className: 'px-3 py-2 text-sm',
                    },
                  },
                }}
              />
              <DatePicker
                label={gameCreationConstants.endDate}
                value={endDate}
                onChange={(newValue) => setEndDate(newValue)}
                slotProps={{
                  textField: {
                    size: 'small',
                    className: 'bg-white rounded-md shadow-sm w-full ',
                    InputProps: {
                      className: 'px-3 py-2 text-sm',
                    },
                  },
                }}
              />
            </div>

            {isLoading ? (
              <Spinner />
            ) : (
              <div>
                {games.length === 0 ? (
                  <p>{gameCreationConstants.noGamesFound}</p>
                ) : (
                  <div className="flex flex-col gap-2 flex-wrap">
                    {games.map((game) => (
                      <LoadGameInfo key={game.Id} game={game} />
                    ))}
                  </div>
                )}
              </div>
            )}
          </div>
        </LocalizationProvider>
      </div>
    </div>
  );
};

export default LoadGame;
