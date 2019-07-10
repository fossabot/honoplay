import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {
    Grid,
    Divider
}
from '@material-ui/core';
import Style from '../Style';
import Typography from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import Input from '../../components/Input/InputTextComponent';
import Table from '../../components/Table/TableComponent';

class Trainers extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            adminUserColumns: [
                { title: "Ad", field: "name" },
                { title: "Soyad", field: "surname" },
                { title: "Kullanıcı Adı", field: "username" },
            ],
            adminUserData: [{
                'id': 0,
                'name': 'Alper',
                'surname': 'Halıcı',
                'username': 'alperhalici'
            },
            ]
        };
    }

    render() {
        const { classes } = this.props;

        return (
            <div className={classes.root}>
                <Grid container spacing={24}>
                    <Typography
                        pageHeader={translate('UserManagement')}
                    />
                    <Grid item xs={12} sm={12} />
                    <Grid item xs={12} sm={12} />
                    <Grid item xs={12} sm={12}>
                        <Input
                            labelName={translate('Name')}
                            inputType="text"
                        />
                        <Input
                            labelName={translate('Surname')}
                            inputType="text"
                        />
                        <Input
                            labelName={translate('UserName')}
                            inputType="text"
                        />
                        <Input
                            labelName={translate('Password')}
                            inputType="text"
                        />
                        <Input
                            labelName={translate('Confirm')}
                            inputType="text"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12} />
                    <Grid item xs={12} sm={11} />
                    <Grid item xs={12} sm={1}>
                        <Button
                            buttonColor="primary"
                            buttonName={translate('Save')}
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <Divider />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <Table
                            columns={this.state.adminUserColumns}
                            data={this.state.adminUserData}
                        />
                    </Grid>
                </Grid>
            </div>
        );
    }
}

export default withStyles(Style)(Trainers);