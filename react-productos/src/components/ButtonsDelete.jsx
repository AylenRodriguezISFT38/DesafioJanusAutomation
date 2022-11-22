import Styles from '../components/Buttons.module.css'

export const Eliminar =({eliminar,path})=>{
    return(
        <button className={Styles['btnEliminar']} onClick={()=>{
           const opcion = confirm("Está seguro de que desea eliminar el dato?");
           if(opcion == true){
              eliminacionProducto(eliminar,path)
           }
        }}>Eliminar</button>
    )
}

const eliminacionProducto = async (eliminar,path) =>{
 const req = await fetch(path,{
        method:'DELETE',
        headers:{
            "content-type":"application/json"
        },
        body: JSON.stringify({
            "id":eliminar
        })
    })   
    location.reload();
}
