import React from 'react'
import {useState,useEffect} from 'react'
import Styles from '../components/Producto.module.css'
import {Eliminar} from '../components/ButtonsDelete'
import Modal2 from '../components/Modal2'
import UpdateTipoProducto from '../components/TipoProductoUpdate'


const TipoProducto = () => {

  const [tipoProducto, setTipoProducto] = useState([])
  useEffect(() => {
    fetch('http://localhost:5266/api/TipoProducto/GetTipoProducto')
      .then((response) => {
        return response.json()
      })
      .then((tipoProducto) => {
        setTipoProducto(tipoProducto.msg) 
      })
  }, [])

  return (
    <div className={Styles['container']}>
    <Modal2/>
    <table className={Styles['table']}>
      <thead>
        <tr>
          <th>Tipo de producto</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {tipoProducto.map(tipoProducto => {
          return (
            <tr key={tipoProducto.id}>
              <td>{tipoProducto.descripcion}</td>
              <td>
                <UpdateTipoProducto datos={tipoProducto}/>
                <Eliminar eliminar={tipoProducto.id} path="http://localhost:5266/api/TipoProducto/DeleteTipoProducto"/>
              </td>
            </tr>
          );
        })}
      </tbody>
    </table>
    
  </div>
  )
}

export default TipoProducto;