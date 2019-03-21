import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid, Typography} from '@material-ui/core';
import ButtonComponent from '../components/Button/ButtonComponent';
import InputTextComponent from '../components/InputText/InputTextComponent';

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

  return (
    <div className={classes.root}>
      <Grid container spacing={24}>
        <Grid item sm={7}>
            <Typography variant="h5" className={classes.Typography} >
            Kullanıcı Yönetimi
            </Typography>
        </Grid>
        <Grid item sm={5}>
            <ButtonComponent ButtonColor="primary" 
                             ButtonIcon="file-excel" 
                             ButtonName="Excel'den Aktar"/>          
        </Grid>
        <Grid item sm={12}>
            <Typography variant="body1" gutterBottom>
            You don't have to go it alone. Master the inbound methodology and get the most out of your 
            tools with HubSpot's legendary customer support team and a community of thousands of 
            marketing and sales pros just like you.
            </Typography>       
        </Grid>
        <Grid item sm={12}>
          <InputTextComponent InputName="Ad" 
                              PlaceHolderName="Ad" 
                              InputType="text"/>
          <InputTextComponent InputName="Soyad" 
                              PlaceHolderName="Soyad" 
                              InputType="text"/>
          <InputTextComponent InputName="Kullanıcı Adı" 
                              PlaceHolderName="Kullanıcı Adı" 
                              InputType="text"/>
          <InputTextComponent InputName="Şifre" 
                              InputType="password"/>
          <InputTextComponent InputName="Şifre Tekrar" 
                              InputType="password"/>
        </Grid>
        <Grid item sm={9}></Grid>
        <Grid item sm={3}>
          <ButtonComponent ButtonColor="secondary" ButtonName="Kullanıcı Oluştur"/>
        </Grid>
      </Grid>
    </div>
  );
}
KullaniciYonetimi.propTypes = {
  classes: PropTypes.object.isRequired,
};
export default withStyles(styles)(KullaniciYonetimi);