import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import Form from './Form';


function App() {
  const [state, setState] = useState<{ source: string; target: string, chain: string[] | null, error: any }>({ source: '', target: '', chain: null, error: null });

  const handleSubmit = (pair: { source: string; target: string }) => {
    let url = `https://${process.env.REACT_APP_API_URL}/wordchain`;
    fetch(url, {
      method: 'POST',
      mode: 'cors',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(pair)
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      })
      .then(data => {

        setState({ ...state, error: null, chain: data });
        console.log('Success:', data);
      })
      .catch(error => {
        setState({ ...state, error: error.message, chain: null });
        console.error('Error:', error);
      });

    setState({ ...state, chain: null });
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>Wordchain</h1>
        <Form onSubmit={handleSubmit} />
        <ul>
          {state.chain?.map((word) => (
            <li key={word}>
              {word}
            </li>
          ))}
        </ul>
        <div>
          {!!state.error && <label>{state.error}</label>}
        </div>
      </header>
    </div>
  );
}

export default App;
