import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid, Typography} from '@material-ui/core';
import ButtonComponent from '../components/Button/ButtonComponent';
import InputTextComponent from '../components/InputText/InputTextComponent';
import SearchInputComponent from '../components/InputText/SearchInputComponent';
import TableComponent from '../components/Table/TableComponent';

const styles = theme => ({
  root: {
    flexGrow: 1,
  },
  Typography: {
    margin: theme.spacing.unit,
    color: '#e92428',
    fontWeight: 'bold',
  },
});

function KullaniciYonetimi(props) {
  const { classes } = props;

  const columns = ['Ad', 'Soyad', 'Kullanıcı Adı'];
  const data = [{ 'Ad':'Şaduman',
                  'Soyad': 'Küçük',
                  'Kullanıcı Adı': 'sadumankucuk',
                },
                {
                  'Ad':'Şaduman',
                  'Soyad': 'Küçük',
                  'Kullanıcı Adı': 'sadumankucuk',
                },
                { 'Ad':'Şaduman',
                  'Soyad': 'Küçük',
                  'Kullanıcı Adı': 'sadumankucuk',
                },
                {
                  'Ad':'Şaduman',
                  'Soyad': 'Küçük',
                  'Kullanıcı Adı': 'sadumankucuk',
                },
                { 'Ad':'Şaduman',
                  'Soyad': 'Küçük',
                  'Kullanıcı Adı': 'sadumankucuk',
                },
              ];
  
  return (
    <div className={classes.root}>
      <Grid container spacing={24}>
        <Grid item sm={9}>
            <Typography variant="h5" className={classes.Typography} >
            Kullanıcı Yönetimi
            </Typography>
        </Grid>
        <Grid item sm={3}>
            <ButtonComponent ButtonColor="primary" 
                             ButtonIcon="file-excel" 
                             ButtonName="Excel'den Aktar"/>          
        </Grid>
        <Grid item sm={12}>
          <InputTextComponent LabelName="Ad" 
                              InputId="inputAd" 
                              InputType="text"/>
          <InputTextComponent LabelName="Soyad" 
                              InputId="inputSoyad" 
                              InputType="text"/>
          <InputTextComponent LabelName="Kullanıcı Adı" 
                              InputId="inputKullaniciAdi" 
                              InputType="text"/>
          <InputTextComponent LabelName="Şifre"
                              InputId="inputSifre" 
                              InputType="password"/>
          <InputTextComponent LabelName="Şifre Tekrar" 
                              InputId="inputSifreTekrar"
                              InputType="password"/>
        </Grid>
        <Grid item sm={9}></Grid>
        <Grid item sm={3}>
          <ButtonComponent ButtonColor="secondary"  
                           ButtonName="Kullanıcıyı Oluştur"/>
        </Grid>
        <Grid item xs={12}>
          <SearchInputComponent InputId="inputKullaniciAra"
                                PlaceHolderName="Kullanıcı Ara"/>
        </Grid>
        <Grid item xs={12}>
          <TableComponent columns={columns} 
                          data={data}
                          deneme="true"/>
        </Grid>
      </Grid>
    </div>
  );
}
KullaniciYonetimi.propTypes = {
  classes: PropTypes.object.isRequired,
};
export default withStyles(styles)(KullaniciYonetimi);