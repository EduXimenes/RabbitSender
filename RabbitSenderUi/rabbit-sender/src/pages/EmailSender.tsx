import React, { useState } from 'react';
import axios from 'axios';

const EmailSender: React.FC = () => {
  const [to, setTo] = useState<string>('');
  const [subject, setSubject] = useState<string>('Email de Teste');
  const [body, setBody] = useState<string>('Este é um e-mail de teste enviado pela interface.');
  const [message, setMessage] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const handleSendEmail = async () => {
    if (!to || !subject || !body) {
      setMessage('⚠️ Preencha todos os campos antes de enviar.');
      return;
    }

    setLoading(true);
    setMessage(null);

    try {
      const response = await axios.post('http://localhost:5206/api/email', {
        to,
        subject,
        body
      });

      if (response.status === 202) {
        setMessage('✅ E-mail enviado com sucesso!');
      } else {
        setMessage('⚠️ Algo deu errado ao enviar o e-mail.');
      }
    } catch (error) {
      console.error(error);
      setMessage('❌ Erro ao enviar e-mail.');
    } finally {
      setLoading(false);
    }
  };

  return (
<div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-sky-500 via-blue-600 to-blue-800 px-4">
      <div className="bg-white rounded-2xl shadow-2xl p-8 w-full max-w-md transition-all duration-300">
        <h1 className="text-3xl font-extrabold text-center text-blue-700 mb-6 tracking-tight">
          RabbitSender 📬
        </h1>

        <div className="space-y-4">
          <input
            type="email"
            placeholder="Destinatário (e-mail)"
            value={to}
            onChange={(e) => setTo(e.target.value)}
            className="w-full border border-gray-300 rounded-lg p-3 text-gray-700 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />

          <input
            type="text"
            placeholder="Título do e-mail"
            value={subject}
            onChange={(e) => setSubject(e.target.value)}
            className="w-full border border-gray-300 rounded-lg p-3 text-gray-700 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />

          <textarea
            placeholder="Corpo do e-mail"
            value={body}
            onChange={(e) => setBody(e.target.value)}
            className="w-full border border-gray-300 rounded-lg p-3 text-gray-700 h-28 resize-none focus:outline-none focus:ring-2 focus:ring-blue-500"
          />

          <button
            onClick={handleSendEmail}
            disabled={loading}
            className={`w-full bg-blue-600 hover:bg-blue-700 text-white font-semibold py-3 rounded-lg transition duration-300 ${
              loading ? 'opacity-50 cursor-not-allowed' : ''
            }`}
          >
            {loading ? 'Enviando...' : 'Enviar E-mail'}
          </button>

          {message && (
            <div className="mt-4 text-center text-sm text-gray-800">
              {message}
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default EmailSender;
