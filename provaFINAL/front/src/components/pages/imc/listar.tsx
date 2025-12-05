import { useEffect,useState } from "react";
import IMC from "../../../models/IMC";
import axios from "axios";

function ListarIMC() {
  const [imcs, setImcs] = useState<IMC[]>([]);
    useEffect(() => {
    console.log("O componente foi carregado!");
    buscarIMCAPI();
  }, []);

  async function buscarIMCAPI() {
    try {
      const resposta = await axios.get(
        "http://localhost:5078/api/imc/listar"
      );
      setImcs(resposta.data);
    } catch (error) {
      console.log("Erro na requisição: " + error);
    }
  }
    //O return é a parte visual do componente
    return (
    <div id="listar_imcs">
      <h1>Listar IMCs</h1>
      <table>
        <thead>
          <tr>
            <th>#</th>
            <th>Altura</th>
            <th>Peso</th>
            <th>IMC</th>
            <th>Classificação</th>
            <th>Data da criação</th>
          </tr>
        </thead>
        <tbody>
          {imcs.map((imc) => (
            <tr key={imc.id}>
                <td>{imc.id}</td>
                <td>{imc.altura}</td>
                <td>{imc.peso}</td>
                <td>{imc.imc}</td>
                <td>{imc.classificacao}</td>
                <td>{imc.dataCriacao}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
    );
}

export default ListarIMC;