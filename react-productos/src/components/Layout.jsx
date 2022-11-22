
import {Outlet, Link} from "react-router-dom"
import Styles from './Layout.module.css'
import {Footer} from './Footer.jsx'

const Layout = () =>{
    return (
        <div>
            <nav className={Styles['NavMenu']}>
              
                <li>
                    <Link to="/">Producto</Link>
                </li>
                <li>
                    <Link to="/tipoproducto">Tipos de producto</Link>
                </li>
            </nav>
            <hr/>
            <Outlet/>
            <Footer/>
        </div>
    )
}

export default Layout;