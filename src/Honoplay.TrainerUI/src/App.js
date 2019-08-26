import React from "react";
import PageWrapper from "./Containers/PageWrapper";
import { Logo, Vector } from "./Assets/index";

function App() {
  return (
    <PageWrapper>
      <div class="col-sm-7">
        <img src={Logo} class="img-fluid" />
        <p class="mt-3 mb-5">
          <b>Omega Bigdata</b> Web Portalına Hoş Geldiniz.
        </p>

        <div class="form">
          <input
            type="text"
            class="form-control"
            placeholder="Cep Tel / E-Posta"
          />
          <input
            type="password"
            class="form-control mt-3 mb-2"
            placeholder="Şifre"
          />

          <a href="#" class="su">
            <u>Şifremi Unuttum</u>
          </a>

          <button class="btn my-btn form-control mt-4">Giriş Yap</button>
        </div>
      </div>
      <div class="col-sm-5 text-center">
        <img src={Vector} class="img-fluid d-none d-sm-block" />
      </div>
    </PageWrapper>
  );
}
export default App;
