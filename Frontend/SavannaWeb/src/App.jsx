import React from 'react'
import { BrowserRouter, Routes, Route } from 'react-router'
import routes from './constants/routes'
import Home from './pages/Home'
import Login from './pages/auth/Login'
import Register from './pages/auth/Register'
import Savanna from './pages/game/Savanna'
import Error from './pages/error/Error'
import NoAccess from './pages/error/NoAccess'
import Layout from './pages/Layout'

const App = () => {
  return (
    <div>
      <BrowserRouter>
      <Routes>
        <Route path={routes.home} element={<Layout/>}>
          <Route index element={<Home/>}/>
          <Route path={routes.login} element={<Login/>}/>
          <Route path={routes.register} element={<Register/>}/>
          <Route path={routes.savanna} element={<Savanna/>}/>
          <Route path={routes.error} element={<Error/>}/>
          <Route path={routes.noAccess} element={<NoAccess/>}/>
        </Route>
      </Routes>
      </BrowserRouter>
    </div>
  )
}

export default App