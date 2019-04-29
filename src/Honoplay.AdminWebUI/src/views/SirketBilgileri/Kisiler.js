import React from 'react';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid, Typography} from '@material-ui/core';
import Style from '../Sorular/Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import DropDown from '../../components/Input/DropDownInputComponent';

class Kisiler extends React.Component {

constructor(props) {
  super(props);
  this.state = {
    calismaDurumu: [
        { key: 1, label: 'Çalışan',  },
        { key: 2, label: 'Aday',  },
        { key: 3, label: 'Stajyer',  },
    ],
    kisidepartmanData: [
        { key: 1, label: 'Tasarım', }
    ],
    cinsiyet: [
      { key: 1, label: 'Erkek', },
      { key: 2, label: 'Kadın', }
    ]};
}

render() {
  const { classes } = this.props;
  
    return (
        <div className={classes.root}>
        <Grid container spacing={40}>
          <Grid item xs={6} sm={9}>
            <Typography variant="subtitle1">Kişi Ekle</Typography>
          </Grid>
          <Grid item xs={6} sm={3}>
            <Button buttonColor="primary" 
                    buttonIcon="file-excel" 
                    buttonName="Excel'den Aktar"
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <DropDown data={this.state.calismaDurumu}
                      labelName="Çalışma Durumu"
            />
            <Input labelName="Ad" 
                   inputId="inputKisiAd" 
                   inputType="text"
            />
            <Input labelName="Soyad" 
                   inputId="inputKisiSoyad" 
                   inputType="text"
            />
            <DropDown data={this.state.kisidepartmanData}
                      labelName="Departman"
            />
            <Input labelName="TCKN" 
                   inputId="inputTCKN" 
                   inputType="text"
            />
            <Input labelName="Cep Telefonu" 
                   inputId="inputTel" 
                   inputType="text"
            />
            <DropDown data={this.state.cinsiyet}
                      labelName="Cinsiyet"
            />
          </Grid>
        </Grid>
        </div>
        );
    }
}

export default withStyles(Style)(Kisiler);