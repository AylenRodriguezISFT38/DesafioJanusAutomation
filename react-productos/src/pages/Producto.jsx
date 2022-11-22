
import {useState,useEffect} from 'react'
import {Eliminar} from '../components/ButtonsDelete'
import Styles from '../components/Producto.module.css'
import Modal  from './Modales'
import UpdateProducto from '../components/UpdateProducto'


 const Producto = () =>{

    const [productos, setProductos] = useState([])

    useEffect(() => {
        fetch('http://localhost:5266/api/Producto/GetProductos')
          .then((response) => {
            return response.json()
          })
          .then((productos) => {
            setProductos(productos.data)
          })
      }, [])
    
    return (
        <div className={Styles['container']}>
        <Modal/>
        <table className={Styles['table']} >
          <thead>
            <tr>
              <th>Nombre</th>
              <th>Tipo de producto</th>
              <th>Precio</th>
              <th>Cantidad</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {productos.map((productos) => {
              return (
                <tr key={productos.id}>
                  <td>{productos.nombre}</td>
                  <td>{productos.descripcion}</td>
                  <td>${productos.precio}</td>
                  <td>{productos.cantidad}</td>
                  <td>
                    <UpdateProducto datos={productos}/>
                    <Eliminar eliminar={productos.id} path="http://localhost:5266/api/Producto/DeleteProductos/"/>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
        
      </div>
    )

}

export default Producto;
