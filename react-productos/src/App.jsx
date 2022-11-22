import {Routes, Route, BrowserRouter} from 'react-router-dom';
import Layout from './components/Layout.jsx';
import Producto from './pages/Producto.jsx';
import TipoProducto from './pages/TipoProducto.jsx'
import Default from './pages/Default.jsx'

const App = ()=>{
  return(
<div>
    <BrowserRouter>
      <Routes>
        <Route element={<Layout/>}>
          <Route path="/" element={<Producto/>}/>
          <Route path="tipoproducto" element={<TipoProducto/>}/>
          <Route path="*" element={<Default/>}/>
        </Route>
      </Routes>
    </BrowserRouter>
  </div>

  )
}

export default App;