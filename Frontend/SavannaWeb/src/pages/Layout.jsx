import React from 'react'
import Header from '../components/layout/Header'
import Footer from '../components/layout/Footer'
import { Outlet } from 'react-router'

const Layout = () => {

  return (
    <div className="flex flex-col min-h-screen">
      <Header/>
      <main className="flex flex-grow container mx-auto">
        <Outlet/>
      </main>
      <Footer/>
    </div>
  )
}

export default Layout;


