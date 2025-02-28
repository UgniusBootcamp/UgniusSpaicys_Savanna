import { BrowserRouter, Route, Routes } from 'react-router';
import routes from './constants/routes';
import Login from './pages/auth/Login';
import Register from './pages/auth/Register';
import Error from './pages/error/Error';
import NoAccess from './pages/error/NoAccess';
import NotFound from './pages/error/NotFound';
import Savanna from './pages/game/Savanna';
import Home from './pages/Home';
import Layout from './pages/Layout';

const App = () => {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path={routes.home} element={<Layout />}>
            <Route index element={<Home />} />
            <Route path={routes.login} element={<Login />} />
            <Route path={routes.register} element={<Register />} />
            <Route path={routes.savanna} element={<Savanna />} />
            <Route path={routes.error} element={<Error />} />
            <Route path={routes.noAccess} element={<NoAccess />} />
            <Route path={routes.NotFound} element={<NotFound />} />
            <Route path="*" element={<NotFound />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
};

export default App;
