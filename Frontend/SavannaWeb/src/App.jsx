import React, { Suspense } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router';
import { AuthProvider } from './auth/AuthProvider';
import { Spinner } from './components/common/Spinner';
import PrivateRoute from './components/routes/PrivateRoute';
import PublicRoute from './components/routes/PublicRoute';
import routes from './constants/routes';

const Login = React.lazy(() => import('./pages/auth/Login'));
const Register = React.lazy(() => import('./pages/auth/Register'));
const Error = React.lazy(() => import('./pages/error/Error'));
const NoAccess = React.lazy(() => import('./pages/error/NoAccess'));
const NotFound = React.lazy(() => import('./pages/error/NotFound'));
const Savanna = React.lazy(() => import('./pages/game/Savanna'));
const Layout = React.lazy(() => import('./pages/Layout'));
const Home = React.lazy(() => import('./pages/Home'));

const App = () => {
  return (
    <div>
      <BrowserRouter>
        <AuthProvider>
          <Suspense fallback={<Spinner />}>
            <Routes>
              <Route path={routes.home} element={<Layout />}>
                <Route index element={<PublicRoute element={<Home />} />} />
                <Route
                  path={routes.login}
                  element={<PublicRoute element={<Login />} />}
                />
                <Route
                  path={routes.register}
                  element={<PublicRoute element={<Register />} />}
                />
                <Route
                  path={routes.savanna}
                  element={<PrivateRoute element={<Savanna />} />}
                />
                <Route path={routes.error} element={<Error />} />
                <Route path={routes.noAccess} element={<NoAccess />} />
                <Route path={routes.notFound} element={<NotFound />} />
                <Route path="*" element={<NotFound />} />
              </Route>
            </Routes>
          </Suspense>
        </AuthProvider>
      </BrowserRouter>
    </div>
  );
};

export default App;
