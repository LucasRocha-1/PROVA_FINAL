import React from 'react';
import CadastrarIMC from './components/pages/imc/cadastrar';
import ListarIMC from './components/pages/imc/listar';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Link } from "react-router-dom";
import ListarPorStatus from './components/pages/imc/listarporstatus';

function App() {
  return (
    <div id="app">
      <BrowserRouter>
      <nav>
          <ul>
            <li>
              <Link to="/">Listar IMCs</Link>
            </li>
            <li>
              <Link to="/imc/cadastrar">Cadastrar IMCs</Link>
            </li>
            <li>
              <Link to="/imc/listarporstatus">Listar IMCs por Status</Link>
            </li>
          </ul>
        </nav>
        <Routes>
          <Route path="/" element={<ListarIMC />} />
          <Route path="/imc/cadastrar" element={<CadastrarIMC/>} />
          <Route path="/imc/listarporstatus" element={<ListarPorStatus />} />
        </Routes>
        <footer>
          Rodapé da aplicação
        </footer>
        </BrowserRouter>
    </div>
  );
}
export default App;
